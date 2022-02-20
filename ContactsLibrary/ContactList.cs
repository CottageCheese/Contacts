using System;
using System.Linq;
using ContactsData;
using Csla;

namespace ContactsLibrary
{
    [Serializable]
  public class ContactList : ReadOnlyListBase<ContactList, ContactRO>
  {
    [Fetch]
    private void Fetch([Inject]IContactRepository dal)
    {
      IsReadOnly = false;
      var data = dal.Get().Select(d => DataPortal.FetchChild<ContactRO>(d));
      AddRange(data);
      IsReadOnly = true;
    }
  }
}
