import { Button, Typography } from "@mui/material";
import { PracticeLessonData } from "../models/lessonDto";
import { Editor } from "@monaco-editor/react";
import { useState } from "react";
import { useRunCodeMutation } from "../api";

interface Props {
  data: PracticeLessonData;
  editMode?: true;
  onSave?: (templateCode: string) => void;
}

export function PracticeLesson({ data, editMode, onSave }: Props) {
  const [code, setCode] = useState(data?.templateCode ?? "");
  const [output, setOutput] = useState("");
  const [runCode, { isLoading }] = useRunCodeMutation();

  function handleEditorChange(value: string | undefined) {
    setCode(value ?? "");
  }

  async function handleRunCode() {
    try {
      const result = await runCode({ code }).unwrap();
      setOutput(result.result ?? "Нет вывода");
    } catch {
      setOutput("Произошла ошибка при выполнении кода.");
    }
  }

  return (
    <>
      <div className="w-100 border rounded-md p-4 mt-6">
        <Editor
          value={code}
          onChange={handleEditorChange}
          language="csharp"
          height="300px"
          options={{ minimap: { enabled: false } }}
        />
      </div>
      <div className="flex gap-2 my-2 justify-end">
        <div>
          <Button
            color="primary"
            variant="contained"
            onClick={handleRunCode}
            disabled={isLoading}
          >
            {isLoading ? "Выполнение..." : "Запустить"}
          </Button>
        </div>

        {editMode && (
          <div>
            <Button
              color="primary"
              variant="outlined"
              onClick={() => (onSave ? onSave(code) : "")}
            >
              Сохранить
            </Button>
          </div>
        )}
      </div>
      <Typography variant="h6" className="text-left mb-2">
        Вывод:
      </Typography>
      <div className="border min-h-20 rounded-md p-2 text-left bg-gray-50 whitespace-pre-wrap">
        {output || "Нет вывода"}
      </div>
    </>
  );
}
