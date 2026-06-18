# 📚 RAG Knowledge Assistant (.NET + React)

A lightweight Retrieval-Augmented Generation (RAG) system built using **.NET 8 Web API** and **React UI**.

The system reads knowledge documents, chunks them, retrieves relevant content using keyword-based search, and returns grounded answers with citations.

---

## 🚀 Features

- 📄 Document ingestion from folder (Markdown/Text)
- ✂️ Chunking of documents
- 🔍 Keyword-based retrieval system
- 📊 Confidence scoring
- 📌 Source document citations
- 🌐 REST API using .NET 8
- ⚛️ React frontend UI (chat-like interface)

---

## 🏗️ Architecture

```text
Documents (.md files)
        ↓
   Chunking Service
        ↓
 In-Memory Storage
        ↓
  Retrieval Engine
        ↓
   Scoring Logic
        ↓
   /ask API endpoint
        ↓
 Answer + Confidence + Sources
