using Csla.Rules;

namespace StaffLibrary.Shared
{
  public class NoSpaceAllowed : BusinessRule
  {
    public NoSpaceAllowed(Csla.Core.IPropertyInfo primaryProperty)
      : base(primaryProperty)
    { }

    protected override void Execute(IRuleContext context)
    {
      var text = (string)ReadProperty(context.Target, PrimaryProperty);
      if (text.ToLower().Contains(" "))
        context.AddErrorResult("No space allowed");
    }
  }
}
