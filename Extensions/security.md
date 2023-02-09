# Security

Cinode will not accept callback endpoints that are not secured with SSL.

All callback requests include two security headers, `Digest` and `X-Cinode-Signature`.  
It is **highly recommended** to verify the signature to ensure request originality and integrity.

- `Digest: sha-256=`
- `X-Cinode-Signature`

## Verify signature

The signatures is computed by concatenating the full `Digest` header value and request body, then hash/sign result with HMAC SHA256.

The HMAC key is the concatenation of your app's client id and client secret.

```plaintext
key = CONCAT(clientId, ":", clientSecret)

hash = SHA256(request.body)
digest = CONCAT("sha-256=", hash)

message = CONCAT(digest, request.body)
signature = HMAC_SHA256(key, message)
```

The request is authenticated if the values of `Digest` and `X-Cinode-Signature` match the your generated values.

## Samples

- Client id: `my-client-id`
- Client secret: `my-client-secret`.

```http
POST /some/callback/handler/endpoint HTTP/1.1
Digest: sha-256=1Aax8ToBk+WvtLyuDlDFnjdARPumdlgngBFMy7bxmqs=
X-Cinode-Signature: uXfOHzjru9AuXH0zNmU7V6GhoHitfFPCl3usu+Bto3M=

{"someproperty":"somevalue"}
```

## C# sample

```csharp
string ClientId = "my-client-id";
string ClientSecret = "my-client-secret";

void Main()
{
    // Mock request values
    var body = "{\"someproperty\":\"somevalue\"}";
    var digest = "sha-256=1Aax8ToBk+WvtLyuDlDFnjdARPumdlgngBFMy7bxmqs=";
    var signature = "uXfOHzjru9AuXH0zNmU7V6GhoHitfFPCl3usu+Bto3M=";

    if (GenerateSignature(ClientId, ClientSecret, digest, body) != signature)
        throw new BadHttpRequestException("Failed signature verification");
    
    if (GenerateDigest(body) != digest)
        throw new BadHttpRequestException("Failed digest verification");
}

byte[] CreateKey(string clientId, string clientSecret) 
{
    return Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}");
}

string GenerateSignature(string clientId, string clientSecret, string digest, string body)
{
    var key = CreateKey(clientId, clientSecret);
    using var hmac = new HMACSHA256(key);
    
    var digestAndBody = Encoding.UTF8.GetBytes(digest + body);
    var signature = hmac.ComputeHash(digestAndBody);

    return Convert.ToBase64String(signature);
}

string GenerateDigest(string body)
{
    var digestBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(body));
    return Convert.ToBase64String(digestBytes);
}

```
