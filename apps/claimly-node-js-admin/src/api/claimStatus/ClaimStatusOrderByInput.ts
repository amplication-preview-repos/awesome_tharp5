import { SortOrder } from "../../util/SortOrder";

export type ClaimStatusOrderByInput = {
  createdAt?: SortOrder;
  id?: SortOrder;
  status?: SortOrder;
  updatedAt?: SortOrder;
};
