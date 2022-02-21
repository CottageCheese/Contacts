using System;
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

        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(nameof(Email));
        public string Email
        {
            get => GetProperty(EmailProperty);
            private set => LoadProperty(EmailProperty, value);
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
            Email = data.Email;
        }
    }
}