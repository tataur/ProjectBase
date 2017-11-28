using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectBase.DAL.Entities
{
    public class CommonEntity
    {
        [Key]
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
