import { Chip, CircularProgress } from "@mui/material";
import { useLazyGetAttachmentLinkQuery } from "../api";
import { useState } from "react";

interface Props {
  fileUrl: string;
  fileName: string;
  lessonId: string;
  onDelete?: () => void;
}

export default function Attachment({
  fileName,
  fileUrl,
  lessonId,
  onDelete,
}: Props) {
  const [trigger] = useLazyGetAttachmentLinkQuery();
  const [loading, setLoading] = useState(false);

  async function handleClick() {
    setLoading(true);
    try {
      const { result } = await trigger({ lessonId, fileUrl }).unwrap();
      if (result) {
        window.open(result, "_blank");
      }
    } catch (err) {
      console.error("Ошибка получения ссылки:", err);
    } finally {
      setLoading(false);
    }
  }

  return (
    <Chip
      label={fileName}
      onClick={handleClick}
      clickable
      onDelete={onDelete}
      className="w-fit text-left"
      icon={loading ? <CircularProgress size={16} /> : undefined}
    />
  );
}
