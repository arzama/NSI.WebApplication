﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace NSI.DC.TaskRepository
{
    [DataContract]
    class TaskSearchCriteriaDto
    {
        [DataMember]
        public long TaskId { get; set; }

        [DataMember]
        public DateTime? DueDate { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime DateCreated { get; set; }

        [DataMember]
        public DateTime? DateModified { get; set; }

        [DataMember]
        public bool? IsDeleted { get; set; }

        [DataMember]
        public long? UserId { get; set; }
    }
}
