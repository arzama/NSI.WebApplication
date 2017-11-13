﻿using System;
using System.Collections.Generic;

namespace IkarusEntities
{
    public partial class Meeting
    {
        public Meeting()
        {
            UserMeeting = new HashSet<UserMeeting>();
        }

        public long MeetingId { get; set; }
        public DateTime From { get; set; }
        public DateTime? To { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public long CreatedByUserId { get; set; }

        public UserInfo CreatedByUser { get; set; }
        public ICollection<UserMeeting> UserMeeting { get; set; }
    }
}
