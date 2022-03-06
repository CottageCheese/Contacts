using System;
using System.Threading.Tasks;
using ContactsData;
using ContactsData.Interfaces;
using Csla;

namespace ContactsLibrary
{
    [Serializable]
    public class ContactList : ReadOnlyListBase<ContactList, ContactRO>
    {

#region Factory Methods

        public static async Task<ContactList> GetContactListAsync()
        {
            return await DataPortal.FetchAsync<ContactList>();
        }

#endregion

        [Fetch]
        protected void DataPortal_Fetch([Inject] IRepository<ContactDto> dal)
        {
            RaiseListChangedEvents = false;
            IsReadOnly = false;

            var data = dal.Get();

            foreach (var obj in data)
            {
                Add(ContactRO.GetContactROChild(obj));
            }
            IsReadOnly = true;
            RaiseListChangedEvents = true;
        }
    }
}