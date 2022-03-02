using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ContactsData;
using Csla;
using Csla.Data;
using Microsoft.Extensions.Configuration;

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
        protected void DataPortal_Fetch([Inject] IConfiguration configuration, [Inject] IContactRepository dal)
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