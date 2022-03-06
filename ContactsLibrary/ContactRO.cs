using System;
using System.Threading.Tasks;
using ContactsData;
using ContactsData.Interfaces;
using Csla;

namespace ContactsLibrary
{
    [Serializable]
    public class ContactRO : ReadOnlyBase<ContactRO>
    {
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(nameof(Firstname));
        public string Firstname
        {
            get => GetProperty(FirstNameProperty);
            private set => LoadProperty(FirstNameProperty, value);
        }

        public static readonly PropertyInfo<string> LastnameProperty = RegisterProperty<string>(nameof(Surname));
        public string Surname
        {
            get => GetProperty(LastnameProperty);
            private set => LoadProperty(LastnameProperty, value);
        }

        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(nameof(Email));
        public string Email
        {
            get => GetProperty(EmailProperty);
            private set => LoadProperty(EmailProperty, value);
        }

#region Factory Methods

        public static async Task<ContactRO> GetContactROAsync(int contactId)
        {
            return await DataPortal.FetchAsync<ContactRO>(contactId);
        }

        internal static ContactRO GetContactROChild(ContactDto contactDto)
        {
            return DataPortal.FetchChild<ContactRO>(contactDto);
        }

#endregion

        [Fetch]
        private void Fetch(int id, [Inject] IRepository<ContactDto> dal)
        {
            var data = dal.Get(id);
            Fetch(data);
        }

        [FetchChild]
        private void Fetch(ContactDto contactDto)
        {
            LoadProperty(IdProperty, contactDto.Id);
            LoadProperty(FirstNameProperty, contactDto.Firstname);
            LoadProperty(LastnameProperty, contactDto.Lastname);
            LoadProperty(EmailProperty, contactDto.Email);
        }
    }
}