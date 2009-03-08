#region (c)2008 Lokad - New BSD license

// Copyright (c) Lokad 2008 
// Company: http://www.lokad.com
// This code is released under the terms of the new BSD licence

#endregion

using NUnit.Framework;

namespace System.Rules
{
	[TestFixture]
	public sealed class StringIsTests
	{
		const string validEmails =
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

		const string invalidEmails =
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
		//TLDDoesntExist@domain.moc
		[Test]
		public void ValidEmail_Positives()
		{
			var split = validEmails.Split(new[] {Environment.NewLine},
				StringSplitOptions.RemoveEmptyEntries);
			
			RuleAssert.For<string>(StringIs.ValidEmail)
				.ExpectNone(split);
		}

		[Test]
		public void ValidEmail_Negatives()
		{
			var split = invalidEmails.Split(new[] {Environment.NewLine},
				StringSplitOptions.RemoveEmptyEntries);

			RuleAssert.For<string>(StringIs.ValidEmail)
				.ExpectError(split);
		}


		[Test]
		public void Limited_X_Y()
		{
			RuleAssert.For(StringIs.Limited(1, 3))
				.ExpectNone("A","AA","AAA")
				.ExpectError(null,"","AAAA");
		}

		[Test]
		public void Limited_X()
		{
			RuleAssert.For(StringIs.Limited(3))
				.ExpectNone("","A", "AA", "AAA")
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
	}
}