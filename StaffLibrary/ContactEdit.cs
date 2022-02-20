using System;
using System.ComponentModel.DataAnnotations;
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

    public static readonly PropertyInfo<string> EmailAddressProperty = RegisterProperty<string>(nameof(EmailAddress));
    [Required]
    public string EmailAddress
    {
        get => GetProperty(EmailAddressProperty);
        set => SetProperty(EmailAddressProperty, value);
    }

    protected override void AddBusinessRules()
    {
      base.AddBusinessRules();
      BusinessRules.AddRule(new InfoText(FirstnameProperty, "Firstname (required)"));
      BusinessRules.AddRule(new CheckCase(FirstnameProperty));
      BusinessRules.AddRule(new NoSpaceAllowed(FirstnameProperty));

      BusinessRules.AddRule(new InfoText(SurnameProperty, "Surname (required)"));
      BusinessRules.AddRule(new CheckCase(SurnameProperty));
      BusinessRules.AddRule(new NoSpaceAllowed(SurnameProperty));
    }

    [Create]
    private void Create()
    {
      Id = -1;
      BusinessRules.CheckRules();
    }

    [Fetch]
    private void Fetch(int id, [Inject]IContactRepository dal)
    {
      var data = dal.Get(id);
      using (BypassPropertyChecks)
        Csla.Data.DataMapper.Map(data, this);
      BusinessRules.CheckRules();
    }

    [Insert]
    private void Insert([Inject]IContactRepository dal)
    {
      using (BypassPropertyChecks)
      {
        var data = new ContactEntity
        {
          Firstname = Firstname,
          Surname = Surname,
          EmailAddress = EmailAddress
          
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
          Surname = Surname,
          EmailAddress = EmailAddress
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
