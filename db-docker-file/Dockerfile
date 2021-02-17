FROM mcr.microsoft.com/mssql/server:2017-latest

COPY ./Reactivities.bak /usr/app/
COPY ./restore.sql /usr/app/
COPY --chown=root:root ./restore.sh /usr/app/
WORKDIR /usr/app/

USER root
RUN chmod 755 restore.sh
RUN ./restore.sh restore.sql

EXPOSE 1433