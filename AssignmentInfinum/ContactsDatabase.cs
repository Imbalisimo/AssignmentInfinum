using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentInfinum
{
    public class ContactsDatabase
    {
        ContactsEntities1 contactsEntities;
        System.Data.Common.DbConnection connection;

        public static ContactsDatabase Instance { get; } = new ContactsDatabase();

        private ContactsDatabase()
        {
            contactsEntities = new ContactsEntities1();
            connection = contactsEntities.Database.Connection;

            connection.Open();
        }

        public List<contacts> GetAllContacts()
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM contacts";

            List<contacts> contactsList = new List<contacts>();

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    contacts contact = new contacts();
                    contact.name = reader.GetValue(0).ToString();
                    contact.birthdate = DateTime.Parse(reader.GetValue(1).ToString());
                    contact.address = reader.GetValue(2).ToString();
                    contactsList.Add(contact);
                }
            }
            return contactsList;
        }

        public contacts GetContact(string name, string address)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM CONTACTS WHERE name = '" + name + "' AND address = '" + address + "'";

            contacts contact = new contacts();
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    contact.name = reader.GetValue(0).ToString();
                    contact.birthdate = DateTime.Parse(reader.GetValue(1).ToString());
                    contact.address = reader.GetValue(2).ToString();
                }
            }
            return contact;
        }

        public int InsertNewContact(contacts contact)
        {
            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO contacts (name, birthdate, address) " +
                                    "VALUES ('" + contact.name + "', "
                                    + DateToString(contact.birthdate) + ", "
                                    + "'" + contact.address + "')";

            try
            {
                int aff = command.ExecuteNonQuery();
                return aff;
            }
            catch
            {
                return 0;
            }
        }

        private string DateToString(DateTime date)
        {
            return "'" + date.Year + "-" + date.Month + "-" + date.Day + "'";
        }

        public int DeleteContact(contacts contact)
        {
            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM contacts WHERE name = '"
                                    + contact.name + "' AND address = '" + contact.address + "'";

            // return value of ExecuteNonQuery (i) is the number of rows affected by the command
            return command.ExecuteNonQuery();
        }

        public int UpdateContact(contacts contactToUpdate, contacts newContact)
        {
            var command = connection.CreateCommand();
            command.CommandText = "UPDATE contacts SET name = '" + newContact.name +
                                    "', birthdate = " + DateToString(newContact.birthdate) +
                                    ", address = '" + newContact.address + 
                                    "' WHERE name = '" + contactToUpdate.name +
                                    "' AND address = '" + contactToUpdate.address + "'";

            // return value of ExecuteNonQuery (i) is the number of rows affected by the command
            return command.ExecuteNonQuery();
        }

        public contacts GetPhoneNumbersForContact(contacts contact)
        {
            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM phonenumbers" + 
                                    " WHERE name = '" + contact.name + "'";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    phonenumbers phonenumber = new phonenumbers();
                    phonenumber.number = reader.GetValue(0).ToString();
                    phonenumber.name = reader.GetValue(1).ToString();
                    //phonenumber.contacts = contact;
                    contact.phonenumbers.Add(phonenumber);
                }
            }
            return contact;
        }
    }
}