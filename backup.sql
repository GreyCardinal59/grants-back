--
-- PostgreSQL database dump
--

-- Dumped from database version 16.4
-- Dumped by pg_dump version 16.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Grants; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Grants" (
    "Id" integer NOT NULL,
    "Title" text,
    "SourceUrl" text,
    "ProjectDirections" jsonb,
    "Amount" integer,
    "LegalForms" jsonb,
    "Age" integer,
    "CuttingOffCriteria" jsonb
);


ALTER TABLE public."Grants" OWNER TO postgres;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Users" (
    "Id" uuid,
    "Login" text,
    "PasswordHash" text
);


ALTER TABLE public."Users" OWNER TO postgres;

--
-- Data for Name: Grants; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Grants" ("Id", "Title", "SourceUrl", "ProjectDirections", "Amount", "LegalForms", "Age", "CuttingOffCriteria") FROM stdin;
4	Грантовый конкурс для преподавателей магистратуры	https://fondpotanin.ru/competitions/professors-grants/	[3, 2, 1]	123	[1, 2, 0]	12	[0, 1, 2, 3]
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Users" ("Id", "Login", "PasswordHash") FROM stdin;
152d8c89-b9fd-4983-b444-e10d3f32d36d	string	$2a$11$QfA2FFaTmQJRqsU83WAvOOWJr0xviV0MKlk7pj8w9ouwuM1u/gNCC
6fe01947-f3e4-4709-986d-eee3d0bd8fd4	string	$2a$11$EPG2UeyAep.UfEAAEKdW6eYC1n1lUIe.Jbw7QYvJ.Hx1qKqcKQNQu
1363f4dc-b377-4d99-bdff-5d9e8440dd09	string	$2a$11$QBX0J9Olw.nCNY1h1wF4PO/R/9bXSdfn.PA5SvHX/WQ3y6pSRLTJa
4c223762-52e1-4f06-9902-73363d35ef48	pupa	$2a$11$hh4FYRRKM9pWWN80XFpv3ucbyK9d1/Vu05gkGuaW59LhgvNST65sq
308d9528-140f-4299-a64c-aefe7416f652	kek	$2a$11$1RtnYAJfHhDVyJWASITJ6.ml9cqdlYuSobICL/5gLM.R.qco.OqZG
645098ca-fce5-4b2d-b809-a503eb3c00f4	admin	$2a$11$3TXpMpeblBb7uP3EGoyPNOWjDW5V66nWef5g2R0728Ox0jk1nLOta
\.


--
-- Name: Grants Grants_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Grants"
    ADD CONSTRAINT "Grants_pkey" PRIMARY KEY ("Id");


--
-- PostgreSQL database dump complete
--

