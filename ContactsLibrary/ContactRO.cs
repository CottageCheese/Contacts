using System;
using System.ComponentModel.DataAnnotations;
using ContactsData;
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

        public static readonly PropertyInfo<string> SurnameProperty = RegisterProperty<string>(nameof(Surname));
        public string Surname
        {
            get => GetProperty(SurnameProperty);
            private set => LoadProperty(SurnameProperty, value);
        }

        public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(nameof(EmailAddress));
        public string EmailAddress
        {
            get => GetProperty(EmailAddressProperty);
            private set => LoadProperty(EmailAddressProperty, value);
        }

        [Fetch]
        private void Fetch(int id, [Inject] IContactRepository dal)
        {
            var data = dal.Get(id);
            Fetch(data);
        }

        [FetchChild]
        private void Fetch(ContactEntity data)
        {
            Id = data.Id;
            Firstname = data.Firstname;
            Surname = data.Surname;
            EmailAddress = data.EmailAddress;
        }
    }
}