#!/usr/bin/env bash
set -m
./opt/mssql/bin/sqlservr & ./import-data.sh
fg