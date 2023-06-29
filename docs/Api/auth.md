## Authentication

### Register

#### Request
```js
POST /auth/register
```

```json
{
    "FirstName": "FirstName",
    "LastName": "LastName",
    "Email": "Test@test.com",
    "Password": "123"
}
```


#### Response
```json
{
    "user": {
        "id": "00000000-0000-0000-0000-000000000000",
        "firstName": "FirstName",
        "lastName": "LastName",
        "email": "Test@test.com",
        "password": "123"
    },
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.ey..."
}
```

### Login

#### Request

```js
POST /auth/login
```

```json
{
  "login": "Test@test.com",
  "password": "123"
}
```
#### Response

```json
{
    "user": {
        "id": "00000000-0000-0000-0000-000000000000",
        "firstName": "FirstName",
        "lastName": "LastName",
        "email": "Test@test.com",
        "password": "123"
    },
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.ey..."
}
```