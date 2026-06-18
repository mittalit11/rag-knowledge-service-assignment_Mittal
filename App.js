import { useState } from "react";
import axios from "axios";
import "./App.css";

function App() {
  const [question, setQuestion] = useState("");
  const [loading, setLoading] = useState(false);
  const [result, setResult] = useState(null);

  const askQuestion = async () => {
    if (!question.trim()) return;

    setLoading(true);
    setResult(null);

    try {
      const response = await axios.post(
        "https://localhost:7228/api/knowledge/ask",
        {
          question: question,
        },
      );

      setResult(response.data);
    } catch (err) {
      setResult({
        answer: "Error calling API",
        confidence: 0,
        sources: [],
      });
    }

    setLoading(false);
  };

  return (
    <div className="container">
      <h1>RAG Knowledge Assistant</h1>

      <div className="input-box">
        <textarea
          rows="3"
          placeholder="Ask a question..."
          value={question}
          onChange={(e) => setQuestion(e.target.value)}
        />

        <button onClick={askQuestion} disabled={loading}>
          {loading ? "Thinking..." : "Ask"}
        </button>
      </div>

      {result && (
        <div className="result">
          <h3>Answer</h3>
          <p>{result.answer}</p>

          <div className="meta">
            <p>
              <b>Confidence:</b> {result.confidence}
            </p>

            <p>
              <b>Sources:</b>{" "}
              {result.sources?.length > 0 ? result.sources.join(", ") : "None"}
            </p>
          </div>
        </div>
      )}
    </div>
  );
}

export default App;
