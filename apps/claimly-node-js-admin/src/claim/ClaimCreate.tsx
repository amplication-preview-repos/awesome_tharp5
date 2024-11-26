import * as React from "react";

import {
  Create,
  SimpleForm,
  CreateProps,
  NumberInput,
  ReferenceInput,
  SelectInput,
  DateTimeInput,
  TextInput,
} from "react-admin";

import { ClaimStatusTitle } from "../claimStatus/ClaimStatusTitle";
import { CustomerTitle } from "../customer/CustomerTitle";

export const ClaimCreate = (props: CreateProps): React.ReactElement => {
  return (
    <Create {...props}>
      <SimpleForm>
        <NumberInput label="amount" source="amount" />
        <ReferenceInput
          source="claimStatus.id"
          reference="ClaimStatus"
          label="ClaimStatus"
        >
          <SelectInput optionText={ClaimStatusTitle} />
        </ReferenceInput>
        <ReferenceInput
          source="customer.id"
          reference="Customer"
          label="Customer"
        >
          <SelectInput optionText={CustomerTitle} />
        </ReferenceInput>
        <DateTimeInput label="date" source="date" />
        <TextInput label="description" multiline source="description" />
      </SimpleForm>
    </Create>
  );
};
