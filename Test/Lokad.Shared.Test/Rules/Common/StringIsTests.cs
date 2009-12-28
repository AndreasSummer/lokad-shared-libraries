#region (c)2009 Lokad - New BSD license

// Copyright (c) Lokad 2009 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using System;
using Lokad.Testing;
using NUnit.Framework;

namespace Lokad.Rules
{
	[TestFixture]
	public sealed class StringIsTests
	{

		// ReSharper disable InconsistentNaming
		const string ValidEmails =
			@"l3tt3rsAndNumb3rs@domain.com
has-dash@domain.com
hasApostrophe.o'leary@domain.org
uncommonTLD@domain.museum
uncommonTLD@domain.travel
uncommonTLD@domain.mobi
countryCodeTLD@domain.uk
lettersInDomain@911.com
underscore_inLocal@domain.net
IPInsteadOfDomain@127.0.0.1
subdomain@sub.domain.com
local@dash-inDomain.com
dot.inLocal@foo.com
a@singleLetterLocal.org
singleLetterDomain@x.org
disposable+email@gmail.com";

		const string InvalidEmails =
			@"two@@signs.com
missingDomain@.com
@missingLocal.org
missingatSign.net
missingDot@com
colonButNoPort@127.0.0.1:

someone-else@127.0.0.1.26
.localStartsWithDot@domain.com
localEndsWithDot.@domain.com
two..consecutiveDots@domain.com
domainStartsWithDash@-domain.com
domainEndsWithDash@domain-.com
numbersInTLD@domain.c0m
missingTLD@domain.
! ""#$%(),/;<>[]`|@invalidCharsInLocal.org
invalidCharsInDomain@! ""#$%(),/;<>_[]`|.org
local@SecondLevelDomainNamesAreInvalidIfTheyAreLongerThan64Charactersss.org";

		const string ValidHostNames = 
@"localhost
127.0.0.1
localhost:80
proxy.com:8080
google.com
myserver.com
uncommonTLD.museum
x.org";

		const string InvalidHostNames =
			@"127.0.0.1:


_domain.com
domain_.com
domain.com:
domain.
! ""#$%(),/;<>_[]`|.org
SecondLevelDomainNamesAreInvalidIfTheyAreLongerThan64Charactersss.org";

		static string[] Split(string source)
		{
			return source.Split(new[] { Environment.NewLine },
				StringSplitOptions.RemoveEmptyEntries);
		}

		//TLDDoesntExist@domain.moc
		[Test]
		public void ValidEmail_Positives()
		{
			RuleAssert.For<string>(StringIs.ValidEmail)
				.ExpectNone(Split(ValidEmails));
		}

		[Test]
		public void ValidEmail_Negatives()
		{
			RuleAssert.For<string>(StringIs.ValidEmail)
				.ExpectError(Split(InvalidEmails));
		}

		[Test]
		public void ValidHost_Positives()
		{
			RuleAssert.For<string>(StringIs.ValidServerConnection)
				.ExpectNone(Split(ValidHostNames));
		}

		[Test]
		public void ValidHost_Negatives()
		{
			RuleAssert.For<string>(StringIs.ValidServerConnection)
				.ExpectError(Split(InvalidHostNames));
		}


		[Test]
		public void Limited_X_Y()
		{
			RuleAssert.For(StringIs.Limited(1, 3))
				.ExpectNone("A", "AA", "AAA")
				.ExpectError(null, "", "AAAA");
		}

		[Test]
		public void Limited_X()
		{
			RuleAssert.For(StringIs.Limited(3))
				.ExpectNone("", "A", "AA", "AAA")
				.ExpectError(null, "AAAA");
		}

		[Test]
		public void Without_X()
		{
			RuleAssert.For(StringIs.Without('!'))
				.ExpectNone("", "ABCD", "?@#")
				.ExpectError(null, "!", "ABCD!");
		}

		[Test]
		public void WithoutLeadingWhiteSpace()
		{
			RuleAssert.For(StringIs.WithoutLeadingWhiteSpace)
				.ExpectNone("", "non", "trailing ", "mid dle")
				.ExpectError(null, "\r new line", "\n new line", "\t tab", " space");
		}


		[Test]
		public void WithoutTrailingWhiteSpace()
		{
			RuleAssert.For(StringIs.WithoutTrailingWhiteSpace)
				.ExpectNone("", "non", " leading", "mid dle")
				.ExpectError(null, "new line\r", "new line\n", "tab\t", "space ");
		}

		[Test]
		public void WithoutUppercase()
		{
			RuleAssert.For(StringIs.WithoutUppercase)
				.ExpectNone("", "valid", "another\tvalid")
				.ExpectError("Fail", "\tthis should Fail");
		}
	}
}