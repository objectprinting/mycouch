﻿using System.Net.Http;
using MyCouch.EnsureThat;
using MyCouch.Net;

namespace MyCouch.Requests.Factories
{
    public class CopyDocumentHttpRequestFactory : DocumentHttpRequestFactoryBase
    {
        public CopyDocumentHttpRequestFactory(IDbClientConnection connection) : base(connection) { }

        public virtual HttpRequest Create(CopyDocumentRequest request)
        {
            Ensure.That(request, "request").IsNotNull();

            var httpRequest = CreateFor<CopyDocumentRequest>(new HttpMethod("COPY"), GenerateRequestUrl(request.SrcId, request.SrcRev));

            httpRequest.Headers.Add("Destination", request.NewId);

            return httpRequest;
        }
    }
}