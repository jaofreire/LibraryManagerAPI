
FROM mcr.microsoft.com/mssql/server:2022-latest

USER root

RUN apt-get update
RUN apt-get install -yq gnupg gnupg2 gnupg1 curl apt-transport-https

RUN curl https://packages.microsoft.com/keys/microsoft.asc -o /var/opt/mssql/ms-key.cer
RUN apt-key add /var/opt/mssql/ms-key.cer
RUN curl https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2022.list -o /etc/apt/sources.list.d/mssql-server-2022.list
RUN apt-get update

RUN wget http://http.us.debian.org/debian/pool/main/o/openldap/libldap-2.4-2_2.4.47+dfsg-3+deb10u7_amd64.deb

RUN apt install ./libldap-2.4-2_2.4.47+dfsg-3+deb10u7_amd64.deb

RUN wget http://nz2.archive.ubuntu.com/ubuntu/pool/main/o/openssl/libssl1.1_1.1.1f-1ubuntu2.22_amd64.deb

RUN dpkg -i libssl1.1_1.1.1f-1ubuntu2.22_amd64.deb

RUN apt-get install -y mssql-server-fts

ENTRYPOINT [ "/opt/mssql/bin/sqlservr" ]
