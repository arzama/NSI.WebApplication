﻿using System;
using System.Collections.Generic;
using System.Text;
using NSI.DC.CaseRepository;
using IkarusEntities;

namespace NSI.BLL.Interfaces
{
    public interface ICaseInfoManipulation
    {
        CaseInfo CreateCaseInfo(CaseInfoDto caseInfoDto);
        CaseInfo GetCaseInfoById(int caseId);
        IEnumerable<CaseInfo> GetCaseInfos();
        bool DeleteCaseInfoById(int caseId);
        bool EditCaseInfoById(int caseId, CaseInfoDto caseInfo);
	}
}
