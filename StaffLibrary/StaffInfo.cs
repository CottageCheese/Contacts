using System;
using Csla;
using StaffData;

namespace StaffLibrary
{
    [Serializable]
    public class StaffInfo : ReadOnlyBase<StaffInfo>
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

        public static readonly PropertyInfo<string> SurnameProperty = RegisterProperty<string>(nameof(SurnameProperty));
        public string Surname
        {
            get => GetProperty(SurnameProperty);
            private set => LoadProperty(SurnameProperty, value);
        }

        [Fetch]
        private void Fetch(int id, [Inject] IStaff dal)
        {
            var data = dal.Get(id);
            Fetch(data);
        }

        [FetchChild]
        private void Fetch(StaffEntity data)
        {
            Id = data.Id;
            Firstname = data.Firstname;
            Surname = data.Surname;
        }
    }
}