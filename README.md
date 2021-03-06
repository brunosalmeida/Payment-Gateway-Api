# Payment Gateway Api

### This is a api based solution for a payment gateway.

<br>

# How to run?
<br>

## Aditional steps to run on Windows.

### Open the following files and  change the end of line sequence from CRLF to LF  

  - container/sql/entrypoint.sh
  - container/sql/import-data.sh

<br>

This is neeed because the way how windows and linux or unix base system finish a line of text is different.  

<br>

```
$ docker compose up  or docker-compose up
```

### If you have previous containers run:

```
$ docker compose up --build  or docker-compose up --build
```

# Troubleshoot

On Windows, set COMPOSE_CONVERT_WINDOWS_PATHS=1 environment variable to solve issue reported on
[issue #1829 at docker/for-win repo:](https://github.com/docker/for-win/issues/1829).

    Bash: export COMPOSE_CONVERT_WINDOWS_PATHS=1
    Cmd: set COMPOSE_CONVERT_WINDOWS_PATHS=1
    PowerShell: $Env:COMPOSE_CONVERT_WINDOWS_PATHS=1

# How to use?

This api provides 2 endpoints, one to send a payment and another to retrieve a previous payment made.

## Swagger documentation 
After run the application you can access api swagger documentation by [this link.](http://localhost:5050/swagger/index.html)

## Kibana
After tun the application you can check the log by [this link](http://localhost:5601)

_Examples:_

## POST

```
curl -X 'POST' \
  'http://localhost:5050/v1/payment' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json-patch+json' \
  -d '{
 "amount": 7000,
 "creditCard": {

    "name": "Bruno Almeida",
    "number": "346861195076250",
    "month": 10,
    "year": 2023,
    "cvv": "123"
    }
}
```

<br>
<br>

## POST respose

```
{
  "id": "91a29a64-0b6a-4f2c-a09e-d002470d92f0",
  "status": "Error"
}
```

<br>
Where ID is the identification code for a transaction.

<br>
<br>

## GET

```
curl -X 'GET' \
  'https://localhost:5001/v1/payment/91a29a64-0b6a-4f2c-a09e-d002470d92f0' \
  -H 'accept: application/json'
```

<br>
<br>

## GET RESPONSE

```
{
  "id": "5b6fe8e3-fbcb-4bc5-89be-f6feaaccac49",
  "amount": 7000,
  "name": "Bruno Almeida",
  "number": "XXXXXXXXXXX6250",
  "month": 10,
  "year": 3000,
  "cvv": "123",
  "status": "Success",
  "createdDate": "2021-05-30T22:00:38.817"
}
```

# Features:

- [x] Caching
- [x] Logging with ELK Stack
- [x] Runing on Containers
- [x] Continous Integration with github actions. 

<br>
<br>
<br>

# Future improvements

- [ ] Api client Sdk for 3th party uses.
- [ ] Resilience for Redis Cache Connection.
- [ ] Resilience for Sql Connection.
- [ ] More unit tests to cover coner cases.
- [ ] Identity give more privacy and security.
- [ ] NGINX as a reverse proxy.
- [ ] Integration Test to cover end to end process.
- [ ] Enable HTTPS.
- [ ] Improve logging.
