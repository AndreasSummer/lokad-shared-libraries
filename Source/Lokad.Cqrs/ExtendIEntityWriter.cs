#region Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.

// Copyright (c) 2009-2010 LOKAD SAS. All rights reserved.
// You must not remove this notice, or any other, from this software.
// This document is the property of LOKAD SAS and must not be disclosed.

#endregion

using System;
using Lokad.Quality;

namespace Lokad.Cqrs
{
	/// <summary>
	/// Extensions for the <see cref="IEntityWriter"/>
	/// </summary>
	[UsedImplicitly]
	public static class ExtendIEntityWriter
	{
		/// <summary>
		/// Updates an entity if the key already exists, or throws <see cref="EntityNotFoundException"/> otherwise.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity to update.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="key">The key of the entity to update.</param>
		/// <param name="patch">The update function.</param>
		/// <exception cref="OptimisticConcurrencyException">when entity being updated had been changed concurrently</exception>
		/// <exception cref="EntityNotFoundException">when entity to update was not found</exception>
		public static void Update<TEntity>(this IEntityWriter store, object key, Action<TEntity> patch)
		{
			store.AddOrUpdate(typeof (TEntity), key,
				k => { throw CqrsErrors.EntityNotFound(typeof (TEntity), k); },
				(k, value) =>
					{
						patch((TEntity) value);
						return value;
					});
		}

		/// <summary>
		/// Updates an entity of the key already exists or creates a new entity otherwise.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="key">The identity of the entity to patch.</param>
		/// <param name="updateValue">The function used to generate a new entity for an existing key based on the key's existing value.</param>
		/// <param name="addValue">Entity factory, if it were not found.</param>
		/// <exception cref="OptimisticConcurrencyException">when update entity had been changed concurrently</exception>
		public static void AddOrUpdate<TEntity>(this IEntityWriter store, object key, Func<TEntity> addValue,
			Action<TEntity> updateValue)
		{
			store.AddOrUpdate(typeof (TEntity),
				key,
				x => addValue(),
				(o, value) =>
					{
						updateValue((TEntity) value);
						return value;
					}
				);
		}

		/// <summary>
		/// If the entity exists for a given key, updates it; otherwise - inserts a new one.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="key">The key.</param>
		/// <param name="entity">The entity to upsert.</param>
		/// <exception cref="OptimisticConcurrencyException">when updated entity had been changed concurrently</exception>
		public static void AddOrUpdate<TEntity>(this IEntityWriter store, object key, TEntity entity)
		{
			store.AddOrUpdate(typeof (TEntity), key, o => entity, (key1, value) => entity);
		}

		/// <summary>
		/// Update the entity with a given key, creating a new one before that, if needed.
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="key">The key.</param>
		/// <param name="updateValue">The function used to update entity.</param>
		/// <param name="ifNotExist">The function used to create new entity before updating, if it does not exist.</param>
		public static void Upsert<TEntity>(this IEntityWriter store, object key, Action<TEntity> updateValue, Func<TEntity> ifNotExist)
		{
			store.AddOrUpdate(typeof(TEntity), key, o =>
				{
					var entity = ifNotExist();
					updateValue(entity);
					return entity;
				}, (key1, value) =>
					{
						updateValue((TEntity) value);
						return value;
					});
		}
		

		/// <summary>
		/// Deletes the specified entity, given it's type and identity
		/// </summary>
		/// <typeparam name="TEntity">The type of the entity.</typeparam>
		/// <param name="store">The store.</param>
		/// <param name="identity">The identity.</param>
		public static void Remove<TEntity>(this IEntityWriter store, string identity)
		{
			store.Remove(typeof (TEntity), identity);
		}
	}
}