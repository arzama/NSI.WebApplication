﻿using NSI.BLL.Interfaces;
using NSI.DC.HearingsRepository;
using NSI.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL
{
    public class HearingsManipulation : IHearingsManipulation
    {
        private readonly IHearingsRepository _hearingsRepository;

        public HearingsManipulation(IHearingsRepository hearingsRepository)
        {
            _hearingsRepository = hearingsRepository;
        }

        public void Create(HearingDto model)
        {
            _hearingsRepository.Insert(model);
        }

        public void Update(int hearingId, HearingDto model)
        {
            _hearingsRepository.Update(hearingId, model);
        }

        public ICollection<HearingDto> GetHearingsByCase(int caseId)
        {
            return _hearingsRepository.GetHearingsByCase(caseId);
        }

        public ICollection<HearingDto> GetHearings()
        {
            return _hearingsRepository.GetHearings();
        }
    }
}
