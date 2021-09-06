using System;
using System.ComponentModel.DataAnnotations;
using InteliSystem.Utils.Globals.Enumerators;
using Utils.Globals.Notifications;

namespace InteliSystem.Utils.Globals.Classes
{
    public abstract class ClassBaseGuid : InteliNotification
    {
        public ClassBaseGuid()
            : base()
        {
            this.Id = Guid.NewGuid();
            this.DateTimeCreate = DateTime.Now;
            this.DateTimeUpdate = DateTime.Now;
            this.Status = StatusValues.Active;
        }

        public ClassBaseGuid(Guid id, StatusValues status = StatusValues.Active)
            : this()
        {
            this.Id = id;
            this.Status = status;
        }
        [Key]
        public virtual Guid Id { get; private set; }
        public StatusValues Status { get; private set; }
        public virtual DateTime DateTimeCreate { get; private set; }
        public virtual DateTime DateTimeUpdate { get; private set; }
    }
}