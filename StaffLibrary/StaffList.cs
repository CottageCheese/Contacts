using System;
using System.Linq;
using Csla;
using StaffData;

namespace StaffLibrary
{
    [Serializable]
  public class StaffList : ReadOnlyListBase<StaffList, StaffInfo>
  {
    [Fetch]
    private void Fetch([Inject]IStaff dal)
    {
      IsReadOnly = false;
      var data = dal.Get().Select(d => DataPortal.FetchChild<StaffInfo>(d));
      AddRange(data);
      IsReadOnly = true;
    }
  }
}
