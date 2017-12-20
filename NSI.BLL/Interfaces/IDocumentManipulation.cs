﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using NSI.DC.DocumentRepository;
using NSI.REST.Models;

namespace NSI.BLL.Interfaces
{
    public interface IDocumentManipulation
    {
        DocumentDto GetDocumentById(int documentId);
        PagingResultModel<DocumentDto> GetDocumentsByPage(DocumentsPagingQueryModel query);
        bool DeleteDocument(int id);
        bool EditDocument(DocumentDto documentDto);
        List<DocumentDto> GetAllDocuments();
        void UploadFile(List<IFormFile> files, string filePath);
        bool SaveDocument(DocumentDto document);
    }
}
