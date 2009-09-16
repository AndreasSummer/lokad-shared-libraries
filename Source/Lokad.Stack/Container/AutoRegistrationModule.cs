//#region (c)2009 Lokad - New BSD license

//// Copyright (c) Lokad 2009 
//// Company: http://www.lokad.com
//// This code is released under the terms of the new BSD licence

//#endregion

//using System;
//using System.Collections.Generic;
//using System.Reflection;
//using Autofac;
//using Autofac.Builder;

//namespace Lokad.Container
//{
//    /// <summary>
//    /// Autofac extension module that provides auto-registration
//    /// capabilities.
//    /// </summary>
//    public sealed class AutoRegistrationModule : IModule
//    {
//        readonly List<Assembly> _references = new List<Assembly>();

//        /// <summary>
//        /// Same as <see cref="AddAssembly"/> but for the XML configs
//        /// </summary>
//        public string IncludeAssemblies
//        {
//            set
//            {
//                value
//                    .Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries)
//                    .ForEach(s => _references.Add(Assembly.LoadFrom(s)));
//            }
//            get { return null; }
//        }


//        /// <summary>
//        /// Adds the assembly to the autoregistration list.
//        /// </summary>
//        /// <param name="assembly"></param>
//        public void AddAssembly(Assembly assembly)
//        {
//            if (assembly == null) throw new ArgumentNullException("assembly");

//            _references.Add(assembly);
//        }

//        /// <summary>
//        /// <see cref="IModule.Configure"/>
//        /// </summary>
//        /// <param name="container"></param>
//        public void Configure(IContainer container)
//        {
//            if (_references.Count > 0)
//            {
//                var builder = new ContainerBuilder();
//                foreach (var assembly in _references)
//                {
//                    builder.AutoRegisterAssembly(assembly);
//                }
//                builder.Build(container);
//            }
//        }
//    }
//}