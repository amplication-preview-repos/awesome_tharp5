import { ClaimCreateNestedManyWithoutCustomersInput } from "./ClaimCreateNestedManyWithoutCustomersInput";

export type CustomerCreateInput = {
  claims?: ClaimCreateNestedManyWithoutCustomersInput;
  email?: string | null;
  name?: string | null;
  phone?: string | null;
};
