using UnityEngine;
using System.Net;
using System.Collections;
using System;
using System.Security.Cryptography.X509Certificates;

public class HTTPSHandshake : ICertificatePolicy {

    // Don't try this at home!

    public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem) {
        return true;
    }
}
