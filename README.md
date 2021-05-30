# Payment Gateway Api

This is a api based solution for a payment gateway.

## How to run?  
<br>

> $ docker compose up

# Thoubleshoot

 On Windows, set COMPOSE_CONVERT_WINDOWS_PATHS=1 environment variable to solve issue reported on 
 [issue #1829 at docker/for-win repo:](https://github.com/docker/for-win/issues/1829).

    Bash: export COMPOSE_CONVERT_WINDOWS_PATHS=1
    Cmd: set COMPOSE_CONVERT_WINDOWS_PATHS=1
    PowerShell: $Env:COMPOSE_CONVERT_WINDOWS_PATHS=1

# How to use?
This api provides 2 endpoints, one to send a payment and another  to retrieve one.

*Examples:*
## POST
> curl -X 'POST' \
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
}'

<br>
<br>

## GET

>curl -X 'GET' \
  'https://localhost:5001/v1/payment/91a29a64-0b6a-4f2c-a09e-d002470d92f0' \
  -H 'accept: application/json'

<br>
<br>

# Features:
- Cache
- Logging with ELK Stack
- Runing on Containers

<br>
<br>
<br>

# Future improvements

- [] Add resilience for Redis Cache.
- [] Add resicilence for Sql Connection.
- [] Add more unit tests to cover coner cases.
- [] Add identity give more privacy and security.
- [] Add NGINX as a reverse proxy.
- [] Create Integration Test to cover end to end process. 
