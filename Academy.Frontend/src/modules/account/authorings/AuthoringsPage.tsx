import { useGetAuthoringsQuery } from "./api";
import { useNavigate } from "react-router";
import AuthoringCard from "./AuthoringCard";

export default function AuthoringsPage() {
  const { data, isLoading, isError } = useGetAuthoringsQuery({});
  const navigate = useNavigate();

  if (isLoading) return <p>Загрузка...</p>;
  if (isError || !data?.result) return <p>Ошибка загрузки заявок</p>;

  return (
    <div className="p-6 flex flex-col gap-3">
      {data.result.map((authoring) => (
        <AuthoringCard
          authoring={authoring}
          onClick={() => {
            navigate(authoring.id);
          }}
        />
      ))}
    </div>
  );
}
