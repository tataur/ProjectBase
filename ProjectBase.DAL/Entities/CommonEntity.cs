using System;

namespace ProjectBase.DAL.Entities
{
    public class CommonEntity
    {
        public Guid Id { get; set; }

        public void FillFieldsOnCreate()
        {
            if(Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }
    }
}
