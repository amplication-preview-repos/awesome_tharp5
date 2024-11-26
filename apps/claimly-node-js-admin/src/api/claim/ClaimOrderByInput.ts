import { SortOrder } from "../../util/SortOrder";

export type ClaimOrderByInput = {
  amount?: SortOrder;
  claimStatusId?: SortOrder;
  createdAt?: SortOrder;
  customerId?: SortOrder;
  date?: SortOrder;
  description?: SortOrder;
  id?: SortOrder;
  updatedAt?: SortOrder;
};
