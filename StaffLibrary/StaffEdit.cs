using System;
using System.ComponentModel.DataAnnotations;
using Csla;
using StaffData;
using StaffLibrary.Shared;

namespace StaffLibrary
{
  [Serializable]
  public class StaffEdit : BusinessBase<StaffEdit>
  {
    public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(nameof(Id));
    public int Id
    {
      get => GetProperty(IdProperty);
      set => SetProperty(IdProperty, value);
    }

    public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(nameof(FirstName));
    [Required]
    public string FirstName
    {
      get => GetProperty(FirstNameProperty);
      set => SetProperty(FirstNameProperty, value);
    }

    public static readonly PropertyInfo<string> SurnameProperty = RegisterProperty<string>(nameof(Surname));
    [Required]
    public string Surname
    {
      get => GetProperty(SurnameProperty);
      set => SetProperty(SurnameProperty, value);
    }

    protected override void AddBusinessRules()
    {
      base.AddBusinessRules();
      BusinessRules.AddRule(new InfoText(FirstNameProperty, "Firstname (required)"));
      BusinessRules.AddRule(new CheckCase(FirstNameProperty));
      BusinessRules.AddRule(new NoSpaceAllowed(FirstNameProperty));

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
    private void Fetch(int id, [Inject]IStaff dal)
    {
      var data = dal.Get(id);
      using (BypassPropertyChecks)
        Csla.Data.DataMapper.Map(data, this);
      BusinessRules.CheckRules();
    }

    [Insert]
    private void Insert([Inject]IStaff dal)
    {
      using (BypassPropertyChecks)
      {
        var data = new StaffEntity
        {
          Firstname = FirstName,
          Surname = Surname
          
        };
        var result = dal.Insert(data);
        Id = result.Id;
      }
    }

    [Update]
    private void Update([Inject]IStaff dal)
    {
      using (BypassPropertyChecks)
      {
        var data = new StaffEntity
        {
          Id = Id,
          Firstname = FirstName,
          Surname = Surname
        };
        dal.Update(data);
      }
    }

    [DeleteSelf]
    private void DeleteSelf([Inject]IStaff dal)
    {
      Delete(ReadProperty(IdProperty), dal);
    }

    [Delete]
    private void Delete(int id, [Inject]IStaff dal)
    {
      dal.Delete(id);
    }

  }
}
