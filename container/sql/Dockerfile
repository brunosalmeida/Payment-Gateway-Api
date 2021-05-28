FROM mcr.microsoft.com/mssql/server:2019-latest

USER root

COPY entrypoint.sh entrypoint.sh
COPY import-data.sh import-data.sh
COPY setup.sql setup.sql

RUN chmod +x entrypoint.sh

CMD /bin/bash ./entrypoint.sh