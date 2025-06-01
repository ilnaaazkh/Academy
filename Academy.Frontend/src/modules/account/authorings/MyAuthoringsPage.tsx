import { useNavigate } from "react-router";
import { useCreateAuthoringMutation } from "./api";
import MyAuthoringsList from "./MyAuthoringsList";

export default function MyAuthoringsPage() {
  const [createAuthoring, { isLoading }] = useCreateAuthoringMutation();
  const navigate = useNavigate();

  function onCreateClick() {
    createAuthoring({ comment: "" })
      .unwrap()
      .then((res) => {
        navigate(`/profile/my-authorings/${res.result}`);
      })
      .catch((err) => console.log(err));
  }

  return (
    <div className="p-6">
      <MyAuthoringsList />

      <div className="flex flex-col items-center justify-center min-h-[60vh] gap-6 text-center">
        <div className="flex gap-4 mt-4">
          <button
            disabled={isLoading}
            onClick={onCreateClick}
            className="px-6 py-3 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
          >
            {isLoading ? "Загрузка..." : "Создать заявку"}
          </button>
        </div>

        <div className="mt-8 text-sm text-gray-500">
          Среднее время рассмотрения заявки - 2 рабочих дня
        </div>
      </div>
    </div>
  );
}
