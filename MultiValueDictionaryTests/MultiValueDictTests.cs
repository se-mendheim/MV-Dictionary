using Microsoft.VisualStudio.TestTools.UnitTesting;
using MV_Dictionary;
using System.Collections.Generic;

namespace MultiValueDictionaryTests
{
	[TestClass]
	public class MultiValueDictTests
	{
		#region KEYS tests
		[TestMethod]
		public void NoKEYSTest()
		{
			string expected = "(empty set)";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.KEYS();

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OneKEYSTest()
		{
			string key = "foo";
			string member = "bar";
			// NOTE for all tests checking the number of items I utilized \n which means the expected
			// number will always be 1 greater than actual due to how the output is written (i.e. newline 
			// at the end of every line makes the count 1 greater)
			int expected = 2;

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);

			int actual = testDict.KEYS().Split("\n").Length;

			Assert.AreEqual(expected, actual);
			
		}

		[TestMethod]
		public void OneThousandKEYSTest()
		{
			string key = "";
			string member = "bar";
			int expected = 1001;

			MultiValueDictionary testDict = new MultiValueDictionary();

			for (int i = 0; i < 1000; i++)
			{
				key = "foo" + i;
				testDict.ADD(key, member);
			}

			int actual = testDict.KEYS().Split("\n").Length;

			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region MEMBERS tests

		[TestMethod]
		public void KeyDoesNotExistMEMBERSTEST()
		{
			string expected = ") ERROR, key does not exist";
			string key = "foo";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.MEMBERS(key);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OneMemberExistsMEMBERSTEST()
		{
			string key = "foo";
			string member = "bar";
			int expected = 2;

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);

			int actual = testDict.MEMBERS(key).Split('\n').Length;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void OneThousandMembersExistMEMBERSTEST()
		{
			string key = "foo";
			string member = "bar";
			int expected = 1001;

			MultiValueDictionary testDict = new MultiValueDictionary();

			for (int i = 0; i < 1000; i++)
			{
				testDict.ADD(key, member + i);
			}

			int actual = testDict.MEMBERS(key).Split('\n').Length;

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region ADD tests
		[TestMethod]
		public void AddOneElementADDTEST()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") Added";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.ADD(key, member);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void AddTwoElementsADDTEST()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") ERROR, member already exists for key";

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);
			string actual = testDict.ADD(key, member);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void EmptyMemberInputADDTEST()
		{
			string key = "foo";
			string member = "";
			string expected = ") ERROR, member cannot be empty";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.ADD(key, member);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void MultiWordPerMemberADDTEST()
		{
			string key = "foo";
			string member = "bar bar bar baz";
			string expected = ") Added";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.ADD(key, member);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void MultipleMembersADDTEST()
		{
			string key = "foo";
			string firstMember = "bar";
			string secondMember = "baz";
			int expected = 3;

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, firstMember);
			testDict.ADD(key, secondMember);

			int actual = testDict.ALLMEMBERS().Split('\n').Length;

			Assert.AreEqual(expected, actual);
		}
		#endregion

		#region REMOVE tests
		[TestMethod]
		public void CorrectlyREMOVETEST()
		{
			string key = "foo";
			string firstMember = "bar";
			string secondMember = "baz";

			int expected = 2;

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, firstMember);
			testDict.ADD(key, secondMember);

			testDict.REMOVE(key, firstMember);

			int actual = testDict.MEMBERS(key).Split('\n').Length;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void KeyDoesNotExistREMOVETEST()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") ERROR, key does not exist";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.REMOVE(key, member);

			Assert.AreEqual(expected, actual);

		}

		[TestMethod]
		public void MemberDoesNotExistREMOVETEST()
		{
			string key = "foo";
			string member = "bar";
			string fakeMember = "baz";
			string expected = ") ERROR, member does not exist";

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);
			string actual = testDict.REMOVE(key, fakeMember);

			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region REMOVEALL tests

		[TestMethod]
		public void CorrectlyRemoveAllREMOVETEST()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") Removed";

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);

			string actual = testDict.REMOVEALL(key);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void KeyNotExistRemoveAll()
		{
			string key = "foo";
			string expected = ") ERROR, key does not exist";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.REMOVEALL(key);

			Assert.AreEqual(expected, actual);
		}


		#endregion

		#region CLEAR tests

		[TestMethod]
		public void CorrectCleared()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") Cleared";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.CLEAR();

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void ConfirmDictCleared()
		{
			string key = "foo";
			string member = "bar";
			int expected = 2;

			MultiValueDictionary testDict = new MultiValueDictionary();

			for (int i = 0; i < 10; i++)
			{
				testDict.ADD(key + i, member + i);
			}

			testDict.CLEAR();

			int actual = testDict.ITEMS().Split().Length;

			Assert.AreEqual(expected, actual);
		}

		


		#endregion

		#region KEYEXISTS tests

		[TestMethod]
		public void KeyExistsTrue()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") true";

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);

			string actual = testDict.KEYEXISTS(key);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void KeyExistsFalse()
		{
			string key = "foo";
			string expected = ") false";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.KEYEXISTS(key);

			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region MEMBEREXISTS tests

		[TestMethod]
		public void MemberExistsTrue()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") true";

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);

			string actual = testDict.MEMBEREXISTS(key, member);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void MemberExistsFalse()
		{
			string key = "foo";
			string member = "bar";
			string fakeMember = "baz";
			string expected = ") false";

			MultiValueDictionary testDict = new MultiValueDictionary();

			testDict.ADD(key, member);

			string actual = testDict.MEMBEREXISTS(key, fakeMember);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void MemberExistsKeyNotFound()
		{
			string key = "foo";
			string member = "bar";
			string expected = ") false";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.MEMBEREXISTS(key, member);

			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region ALLMEMBERS tests

		[TestMethod]
		public void CorrectNumberOfAllMembers()
		{
			string key = "foo";
			string member = "bar";
			int expected = 11;

			MultiValueDictionary testDict = new MultiValueDictionary();

			for (int i = 0; i < 10; i++)
			{
				testDict.ADD(key + i, member + i);
			}

			int actual = testDict.ALLMEMBERS().Split('\n').Length;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void EmptyAllMembers()
		{
			string expected = "(empty set)";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.ALLMEMBERS();

			Assert.AreEqual(expected, actual);
		}

		#endregion

		#region ITEMS tests

		[TestMethod]
		public void CorrectNumberOfItems()
		{
			string key = "foo";
			string member = "bar";
			int expected = 11;

			MultiValueDictionary testDict = new MultiValueDictionary();

			for (int i = 0; i < 10; i++)
			{
				testDict.ADD(key + i, member + i);
			}

			int actual = testDict.ITEMS().Split('\n').Length;

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void EmptyItems()
		{
			string expected = "(empty set)";

			MultiValueDictionary testDict = new MultiValueDictionary();

			string actual = testDict.ITEMS();

			Assert.AreEqual(expected, actual);
		}

		#endregion
	}
}
