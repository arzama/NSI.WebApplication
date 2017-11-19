﻿using NSI.DC.Common;
using NSI.DC.DocumentRepository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NSI.BLL.DocumentRepository
{
    public interface IDocumentManipulation
    {
        DocumentDto GetDocumentById(int documentId);
        ICollection GetCaseDocuments(int caseId);
        ICollection GetCustomerDocuments(int customerId, Paging paging);
        DocumentDto SaveDocument();
    }
}
