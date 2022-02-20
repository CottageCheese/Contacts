using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactsData
{
  public class ContactRepositoryDal : IContactRepository
  {
    private static readonly List<ContactEntity> _contactsTable = new()
    { 
      new ContactEntity { Id = 1, Firstname = "Bill", Surname = "Marks"},
      new ContactEntity { Id = 2, Firstname = "Becky", Surname = "Connor"},
      new ContactEntity { Id = 3, Firstname = "Tam", Surname = "Kane"},
      new ContactEntity { Id = 4, Firstname = "Stephen", Surname = "Smith"},
      new ContactEntity { Id = 5, Firstname = "Ian", Surname = "Jenkins"},
    };

    public bool Delete(int id)
    {
      var person = _contactsTable.FirstOrDefault(p => p.Id == id);
      if (person != null)
      {
        lock (_contactsTable)
          _contactsTable.Remove(person);
        return true;
      }
      else
      {
        return false;
      }
    }

    public bool Exists(int id)
    {
      var person = _contactsTable.FirstOrDefault(p => p.Id == id);
      return person != null;
    }

    public ContactEntity Get(int id)
    {
      var person = _contactsTable.FirstOrDefault(p => p.Id == id);
      if (person != null)
        return person;
      else
        throw new KeyNotFoundException($"Id {id}");
    }

    public List<ContactEntity> Get()
    {
      // return projection of entire list
      return _contactsTable.Where(r => true).ToList();
    }

    public ContactEntity Insert(ContactEntity contact)
    {
      if (Exists(contact.Id))
        throw new InvalidOperationException($"Key exists {contact.Id}");
      lock (_contactsTable)
      {
        int lastId = _contactsTable.Max(m => m.Id);
        contact.Id = ++lastId;
        _contactsTable.Add(contact);
      }
      return contact;
    }

    public ContactEntity Update(ContactEntity contact)
    {
      lock (_contactsTable)
      {
        var old = Get(contact.Id);
        old.Firstname = contact.Firstname;
        old.Surname = contact.Surname;
        return old;
      }
    }
  }
}
