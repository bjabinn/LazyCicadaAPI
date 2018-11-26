CREATE USER misslazycicada WITH PASSWORD 'root';
CREATE DATABASE lazycicada;
GRANT ALL PRIVILEGES ON DATABASE lazycicada TO misslazycicada;
\connect lazycicada;
CREATE SCHEMA headcount AUTHORIZATION misslazycicada;