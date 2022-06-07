using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MV_Dictionary
{
	class Program
	{
		static void Main(string[] args)
		{
			// object used to manipulate a multivalue dictionary
			MultiValueDictionary newDict = new MultiValueDictionary();

			// keep running till user exits
			while (true)
			{
				// input
				Console.Write(">");

				// trim this input so the user cannot input "add foo (empty space)" which would add an empty space as a member
				string command = Console.ReadLine().Trim();
				
				// split the command and parameters
				string[] commandSplit = command.Split();

				// upper case user input to assure commands are recognized
				// NOTE: user parameters are left as the user entered them
				commandSplit[0] = commandSplit[0].ToUpper();
				

				// check if the user input is in valid format
				if (!newDict.VALIDATEINPUT(commandSplit))
				{
					Console.WriteLine("Please enter a valid command (enter HELP for list of commands)");
				}
				// return the list of all keys
				else if (commandSplit[0].Trim().Equals("KEYS"))
				{
					Console.WriteLine(newDict.KEYS());
				}
				// return the list of members for a specific key
				else if (commandSplit[0].Trim().Equals("MEMBERS"))
				{
					Console.WriteLine(newDict.MEMBERS(commandSplit[1]));
				}
				// add a new key and member to the dictionary
				else if (commandSplit[0].Trim().Equals("ADD"))
				{
					string member = newDict.GENERATEMEMBER(commandSplit);
					Console.WriteLine(newDict.ADD(commandSplit[1], member));
				}
				// remove a member associated with a key
				else if (commandSplit[0].Trim().Equals("REMOVE"))
				{
					string member = newDict.GENERATEMEMBER(commandSplit);
					Console.WriteLine(newDict.REMOVE(commandSplit[1], member));
				}
				// remove key from dictionary and all members associated with it
				else if (commandSplit[0].Trim().Equals("REMOVEALL"))
				{
					Console.WriteLine(newDict.REMOVEALL(commandSplit[1]));
				}
				// removes everything from the dictionary
				else if (commandSplit[0].Trim().Equals("CLEAR"))
				{
					Console.WriteLine(newDict.CLEAR());
				}
				// check if a certain key exists in the dictionary
				else if (commandSplit[0].Trim().Equals("KEYEXISTS"))
				{
					Console.WriteLine(newDict.KEYEXISTS(commandSplit[1]));
				}
				// check if a member associated with a specific key exists
				else if (commandSplit[0].Trim().Equals("MEMBEREXISTS"))
				{
					string member = newDict.GENERATEMEMBER(commandSplit);
					Console.WriteLine(newDict.MEMBEREXISTS(commandSplit[1], member));
				}
				// returns all of the members of the dictionary
				else if (commandSplit[0].Trim().Equals("ALLMEMBERS"))
				{
					Console.WriteLine(newDict.ALLMEMBERS());
				}
				// returns all of the keys in the dictionary and all their members
				else if (commandSplit[0].Trim().Equals("ITEMS"))
				{
					Console.WriteLine(newDict.ITEMS());
				}
				// show list of commands and user input
				else if (commandSplit[0].Trim().Equals("HELP"))
				{
					Console.WriteLine("List of commands | '()' = user input:\n\n-KEYS\n-MEMBERS (KEY)\n-ADD (KEY) (MEMBER)\n-REMOVE (KEY) (MEMBER)\n-REMOVEALL (KEY)\n-CLEAR\n-KEYEXISTS (KEY)\n-MEMBEREXISTS (KEY) (MEMBER)\n-ALLMEMBERS\n-ITEMS\n-EXIT\n");
				}
				// exit the program
				else if (commandSplit[0].Trim().Equals("EXIT"))
				{
					break;
				}
				// user did not input any of the commands
				else
				{
					Console.WriteLine("Please enter a valid command (enter HELP for list of commands)");
				}
			}
		}
	}



	public class MultiValueDictionary
	{

		Dictionary<string, HashSet<string>> multiValueDictionary = new Dictionary<string, HashSet<string>>();

		/*-------------------------------------------------------------------------------------------------------------------------
		 * VALIDATEINPUT - this function is utilized to make sure there are enough arguments in user input to properly run commands
		 * 
		 * commandSplit : string array - this parameter is the users input split into a string array of each string entered
		 * ------------------------------------------------------------------------------------------------------------------------*/
		public bool VALIDATEINPUT(string [] commandSplit)
		{
			// for each command: (MEMBERS, REMOVEALL, KEYEXISTS) the user is required to input a key, thus this assures the user has input 2 strings
			if (commandSplit[0].Trim().Equals("MEMBERS") || commandSplit[0].Trim().Equals("REMOVEALL") || commandSplit[0].Trim().Equals("KEYEXISTS"))
			{
				if (commandSplit.Length != 2)
				{
					return false;
				}
			}
			// for each command: (ADD, REMOVE, MEMBEREXISTS) the user is required to input a key and a member, thus this assures the user has input at least 3 strings
			else if (commandSplit[0].Trim().Equals("ADD") || commandSplit[0].Trim().Equals("REMOVE") || commandSplit[0].Trim().Equals("MEMBEREXISTS"))
			{
				if (commandSplit.Length < 3)
				{
					return false;
				}
			}
			// for each command that doesn't require other input, assure the user only puts one word in (KEYS, CLEAR, ALLMEMBERS, ITEMS)
			else if (commandSplit[0].Trim().Equals("KEYS") || commandSplit[0].Trim().Equals("CLEAR") || commandSplit[0].Trim().Equals("ALLMEMBERS") || commandSplit[0].Trim().Equals("ITEMS"))
			{
				if (commandSplit.Length != 1)
				{
					return false;
				}
			}

			// if the input is valid continue to execution of the command
			return true;

		}

		/*-------------------------------------------------------------------------------------------------
		 * GENERATEMEMBER - this function is used in the case that multiple words will be used for a member
		 * 
		 * commandSplit: string [] - this parameter is the command string split into multiple parts
		 *-------------------------------------------------------------------------------------------------*/
		public string GENERATEMEMBER(string [] commandSplit)
		{
			string member = "";
			// start at i = 2 so that the COMMAND [i = 0] and KEY [i = 1] are ignored when generating the member
			for (int i = 2; i < commandSplit.Length; i++)
			{
				member += commandSplit[i] + " ";
			}

			return member;
		}

		/*-------------------------------------------------------------------------------------
		 * KEYS - this fucntion returns all the keys in the dictionary. Order is not guaranteed
		 *-------------------------------------------------------------------------------------*/
		public string KEYS()
		{
			// check if the dictionary contains anything
			if (multiValueDictionary.Count == 0)
			{
				return "(empty set)";
			}

			// variable used to return output
			string allKeysOutput = "";
			// counter utilized only for output formatting
			int counter = 1;

			// create the output to be returned in the format of "1-MAX_INT) member"
			foreach (string key in multiValueDictionary.Keys)
			{
				allKeysOutput += counter + ") " + key + "\n";
				counter++;
			}
			
			return allKeysOutput;
		}

		/*--------------------------------------------------------------------------------------
		 * MEMBERS - this function returns the collection of strings for a given key
		 * 
		 * key : string - this parameter is used to determine which members to return from the dictionary
		 * 
		 * (return order is not guaranteed)
		 * (returns an error if the key does not exist)
		 *--------------------------------------------------------------------------------------*/
		public string MEMBERS(string key)
		{
			// variable used to return output
			string allMembersOutput = "";

			// check if the key exists in the dictionary
			if (multiValueDictionary.ContainsKey(key))
			{
				// counter utilized only for output formatting
				int counter = 1;

				// create the output to be returned in the format of "1-MAX_INT) member"
				foreach (string member in multiValueDictionary[key])
				{
					// create the output to be returned in the format of "1-MAX_INT) member"
					allMembersOutput += counter + ") " + member + "\n";
					counter++;
				}
			}
			// if the key does not exists in the dictionary
			else
			{
				return ") ERROR, key does not exist";
			}

			return allMembersOutput;
		}

		/*-----------------------------------------------------------------------------------------------------
		 * ADD - this function adds a member to the dictionary for the given key
		 * 
		 * key : string - this parameter is the key to add to the multivalue dictionary
		 * member : string - this parameter is a string to add to the list of members associated with the specified key
		 * 
		 * (if key does not exist this method will create a new element in the dictionary)
		 * (if member is null this adds a key with an empty hashset for the members)
		 * (returns an error if the member already exists for the key)
		 *----------------------------------------------------------------------------------------------------*/
		public string ADD(string key, string member)
		{
			// check if the key exists in the dictionary
			if (multiValueDictionary.ContainsKey(key))
			{
				// check to make sure member is not empty
				if (!string.IsNullOrEmpty(member))
				{
					// if the hashset for that specified key already contains the member
					if (multiValueDictionary[key].Contains(member))
					{
						return ") ERROR, member already exists for key";
					}
					// otherwise add the member
					multiValueDictionary[key].Add(member);
				}
				else
				{
					return ") ERROR, member cannot be empty";
				}
			}
			// if the key does not exist in the dictionary
			else
			{
				// check to make sure member is not empty
				if (!string.IsNullOrEmpty(member))
				{
					// add the key and member to a new element in the dictionary
					multiValueDictionary.Add(key, new HashSet<string>());
					multiValueDictionary[key].Add(member);
				}
				else
				{
					return ") ERROR, member cannot be empty";
				}
			}

			return ") Added";
		}

		/*------------------------------------------------------------------------------------------------------------------
		 * REMOVE - this function removes a member from a key 
		 * 
		 * key : string - this parameter is used to find which list of members to remove the specific member
		 * member : string - this parameter is a string to remove from the list of members associated with the specified key
		 * 
		 * (if the last member is removed from the key, the key is removed from the dictionary)
		 * (returns an error if the key or member does not exist)
		 *------------------------------------------------------------------------------------------------------------------*/
		public string REMOVE(string key, string member)
		{
			// check if the key exists in the dictionary
			if (!multiValueDictionary.ContainsKey(key))
			{
				return ") ERROR, key does not exist";
			}
			// if the key does exist in the dictionary
			else
			{
				// check if the member exists for the specified key
				if (!multiValueDictionary[key].Contains(member))
				{
					return ") ERROR, member does not exist";
				}
				// the member does exist
				else
				{
					// check if this is the last member of the hashset
					if (multiValueDictionary[key].Count == 1)
					{
						// remove both the key and it's list of members
						multiValueDictionary.Remove(key);
					}
					else
					{
						// otherwise just remove the member
						multiValueDictionary[key].Remove(member);
					}
				}
			}
			return ") Removed";
		}

		/*------------------------------------------------------------------------------------------------
		 * REMOVEALL - this function removes all members for a key and removes the key from the dictionary
		 * 
		 * key : string - this variable is used to determine what element to remove from the dictionary
		 * 
		 * (returns an error if the key does not exist)
		 *------------------------------------------------------------------------------------------------*/
		public string REMOVEALL(string key)
		{
			// check if the key exists in the dictionary
			if (!multiValueDictionary.ContainsKey(key))
			{
				return ") ERROR, key does not exist";
			}
			// if the does exist in the dictionary
			else
			{
				// remove the key and it's associated list of members
				multiValueDictionary.Remove(key);
			}

			return ") Removed";
		}

		/*---------------------------------------------------------------------------
		 * CLEAR - this function removes all keys and all members from the dictionary
		 *---------------------------------------------------------------------------*/
		public string CLEAR()
		{
			// clear everything from the dictionary
			multiValueDictionary.Clear();
			return ") Cleared";
		}

		/*------------------------------------------------------------------------------------------------
		 * KEYEXISTS - this function returns whether a key exists or not
		 * 
		 * key : string - this variable is used to determine if the specified key exists in the dictionary
		 *------------------------------------------------------------------------------------------------*/
		public string KEYEXISTS(string key)
		{
			// check if the key exists
			if (!multiValueDictionary.ContainsKey(key))
			{
				return ") false";
			}
			else
			{
				return ") true";
			}
		}

		/*-------------------------------------------------------------------------------------------
		 * MEMBEREXISTS - this function returns whether a member exists within a key
		 * 
		 * key : string - this variable is used to determine which element in the dictionary to check
		 * member : string - this variable is used as the member the function is looking for
		 * 
		 * (returns false if the key does not exist)
		 *-------------------------------------------------------------------------------------------*/
		public string MEMBEREXISTS(string key, string member)
		{
			// check if the key does not exist
			if (!multiValueDictionary.ContainsKey(key))
			{
				return ") false";
			}
			else
			{
				// check if the member exists
				if (!multiValueDictionary[key].Contains(member))
				{
					return ") false";
				}
				else
				{
					return ") true";
				}
			}
		}

		/*---------------------------------------------------------------------
		 * ALLMEMBERS - this function returns all the members in the dictionary
		 *
		 * (returns nothing if there are none)
		 * (order is not guarenteed)
		 *---------------------------------------------------------------------*/
		public string ALLMEMBERS()
		{
			// variable used to return output
			string allMemberOutput = "";
			// counter utilized only for output formatting
			int counter = 1;

			// this foreach will go through each value of the dictionary (which are Hashmaps)
			foreach (HashSet<string> members in multiValueDictionary.Values)
			{
				// running through each element of the Hashmap
				foreach (string member in members)
				{
					// create the output to be returned in the format of "1-MAX_INT) member"
					allMemberOutput += counter + ") " + member + "\n";
					counter++;
				}
			}

			if (string.IsNullOrEmpty(allMemberOutput)) 
			{
				return "(empty set)";
			}


			return allMemberOutput;
		}

		/*--------------------------------------------------------------------
		 * ITEMS - returns all keys in the dictionary and all of their members
		 * 
		 * (returns nothing if there are none)
		 * (order is not guarenteed)
		 *--------------------------------------------------------------------*/
		public string ITEMS()
		{
			// check if the dictionary is empty
			if (multiValueDictionary.Count == 0)
			{
				return "(empty set)";
			}

			// variable used to return output
			string allItemsOutput = "";
			// counter utilized only for output formatting
			int counter = 1;

			// run through the dictionary's keys
			foreach (string key in multiValueDictionary.Keys)
			{
				// run through the members of each key
				foreach(string member in multiValueDictionary[key])
				{
					// create the output to be returned in the format of "1-MAX_INT) key : member"
					allItemsOutput += counter + ") " + key + ": " + member + "\n";
					counter++;
				}
			}

			return allItemsOutput;
		}
	}
}
