using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ContactsData;
using CoreBusinessLibrary;
using Csla;

namespace ContactsLibrary
{
    [Serializable]
    public class ContactEdit : BusinessBase<ContactEdit>
    {

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> FirstnameProperty = RegisterProperty<string>(nameof(Firstname));
        [Required]
        public string Firstname
        {
            get => GetProperty(FirstnameProperty);
            set => SetProperty(FirstnameProperty, value);
        }

        public static readonly PropertyInfo<string> SurnameProperty = RegisterProperty<string>(nameof(Surname));
        [Required]
        public string Surname
        {
            get => GetProperty(SurnameProperty);
            set => SetProperty(SurnameProperty, value);
        }

        public static readonly PropertyInfo<string> EmailProperty = RegisterProperty<string>(nameof(Email));
        [Required]
        public string Email
        {
            get => GetProperty(EmailProperty);
            set => SetProperty(EmailProperty, value);
        }

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            BusinessRules.AddRule(new InfoText(FirstnameProperty, "Firstname (required)"));
            BusinessRules.AddRule(new CheckCase(FirstnameProperty));
            BusinessRules.AddRule(new NoSpaceAllowed(FirstnameProperty));

            BusinessRules.AddRule(new InfoText(SurnameProperty, "Lastname (required)"));
            BusinessRules.AddRule(new CheckCase(SurnameProperty));
            BusinessRules.AddRule(new NoSpaceAllowed(SurnameProperty));
        }

#region Factory Methods

        public static async Task<ContactEdit> GetContactEdit(int id)
        {
            return await DataPortal.FetchAsync<ContactEdit>(id);
        }

        #endregion

        [Fetch]
        //protected void DataPortal_Fetch(int id, [Inject] IConfiguration configuration, [Inject] IContactRepository dal)
        protected void DataPortal_Fetch(int id, [Inject] IContactRepository dal)
        {
            var data = dal.Get(id);

            LoadProperty(IdProperty, data.Id);
            LoadProperty(FirstnameProperty, data.Firstname);
            LoadProperty(SurnameProperty, data.Lastname);
            LoadProperty(EmailProperty, data.Email);

            BusinessRules.CheckRules();
        }

        [Create]
        private void Create()
        {
            Id = -1;
            BusinessRules.CheckRules();
        }

        [Insert]
        [Transactional(TransactionalTypes.TransactionScope)]
        //private void Insert([Inject] IConfiguration configuration, [Inject] IContactRepository dal)
        private void Insert([Inject] IContactRepository dal)
        {
            using (BypassPropertyChecks)
            {
                var data = new ContactEntity
                {
                    Firstname = Firstname,
                    Lastname = Surname,
                    Email = Email

                };
                var result = dal.Insert(data);
                Id = result.Id;
            }
        }

        [Update]
        private void Update([Inject]IContactRepository dal)
        {
            using (BypassPropertyChecks)
            {
                var data = new ContactEntity
                {
                Id = Id,
                Firstname = Firstname,
                Lastname = Surname,
                Email = Email
            };
            dal.Update(data);
            }
        }

        [DeleteSelf]
        private void DeleteSelf([Inject]IContactRepository dal)
        {
            Delete(ReadProperty(IdProperty), dal);
        }

        [Delete]
        private void Delete(int id, [Inject]IContactRepository dal)
        {
            dal.Delete(id);
        }
    }
}