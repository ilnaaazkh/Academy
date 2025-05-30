import { useParams } from "react-router";
import { useGetAuthoringQuery } from "./api";
import { skipToken } from "@reduxjs/toolkit/query";

export default function MyAuthoringPage() {
  const { id } = useParams();
  const { data, isLoading, isError } = useGetAuthoringQuery(
    id ? { id } : skipToken
  );

  console.log(id);

  return <div className="p-6">{JSON.stringify(data)}</div>;
}
