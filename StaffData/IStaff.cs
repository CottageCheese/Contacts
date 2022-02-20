using System.Collections.Generic;

namespace StaffData
{
  public interface IStaff
  {
    bool Exists(int id);
    StaffEntity Get(int id);
    List<StaffEntity> Get();
    StaffEntity Insert(StaffEntity staff);
    StaffEntity Update(StaffEntity staff);
    bool Delete(int id);
  }
}
