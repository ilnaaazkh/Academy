import { useParams } from "react-router";
import { useGetLessonQuery } from "./api";
import { skipToken } from "@reduxjs/toolkit/query";
import { Alert, CircularProgress } from "@mui/material";
import ReactMarkdown from "react-markdown";
import remarkGfm from "remark-gfm";
import { Prism as SyntaxHighlighter } from "react-syntax-highlighter";
import { dracula as codeTheme } from "react-syntax-highlighter/dist/esm/styles/prism";
import Attachment from "./components/Attachment";
import { PracticeLesson } from "./components/PracticeLesson";
import TestLesson from "./components/TestLesson";

export default function Lesson() {
  const { lessonId } = useParams<string>();
  const { data, isLoading, isError } = useGetLessonQuery(
    lessonId ? { id: lessonId } : skipToken
  );

  if (isLoading) {
    return <CircularProgress />;
  }

  if (isError) {
    return (
      <div className="h-3/4 flex justify-center items-center m-10">
        <Alert severity="error" variant="outlined">
          Ошибка при получении данных. Попробуйте позже
        </Alert>
      </div>
    );
  }

  return (
    <div className="px-72 py-10">
      <div className="prose text-center max-w-none">
        <ReactMarkdown>
          {data?.result?.title ? "# " + data?.result?.title : ""}
        </ReactMarkdown>
      </div>
      <div className="prose max-w-none text-justify">
        <ReactMarkdown
          components={{
            code({ node, inline, className, children, ...props }) {
              const match = /language-(\w+)/.exec(className || "");
              return !inline && match ? (
                <SyntaxHighlighter
                  children={String(children).replace(/\n$/, "")}
                  style={codeTheme}
                  language={match[1]}
                  PreTag="div"
                  {...props}
                />
              ) : (
                <code className={className} {...props}>
                  {children}
                </code>
              );
            },
          }}
          remarkPlugins={[remarkGfm]}
        >
          {data?.result?.content ?? ""}
        </ReactMarkdown>
      </div>
      {data?.result?.lessonType === "PRACTICE" && (
        <PracticeLesson data={data.result.practiceLessonData} />
      )}
      {data?.result?.lessonType === "TEST" && (
        <TestLesson questions={data.result.questions} />
      )}
      <div className="text-left prose">
        {!!data?.result?.attachments?.length && (
          <h2 className="mb-4">Приложения</h2>
        )}
        <div className="space-x-2">
          {data?.result?.attachments.map((a) => (
            <Attachment
              key={a.fileUrl}
              fileUrl={a.fileUrl}
              lessonId={lessonId ?? ""}
              fileName={a.fileName}
            />
          ))}
        </div>
      </div>
    </div>
  );
}
