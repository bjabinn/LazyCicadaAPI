FROM postgres
ENV HTTP_PROXY "http://10.0.8.102:8080"
ENV HTTPS_PROXY "http://10.0.8.102:8080"
COPY ./script/*.sql /docker-entrypoint-initdb.d/
