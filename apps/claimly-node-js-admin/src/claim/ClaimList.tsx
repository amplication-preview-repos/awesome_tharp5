import * as React from "react";
import {
  List,
  Datagrid,
  ListProps,
  TextField,
  ReferenceField,
  DateField,
} from "react-admin";
import Pagination from "../Components/Pagination";
import { CLAIMSTATUS_TITLE_FIELD } from "../claimStatus/ClaimStatusTitle";
import { CUSTOMER_TITLE_FIELD } from "../customer/CustomerTitle";

export const ClaimList = (props: ListProps): React.ReactElement => {
  return (
    <List {...props} title={"Claims"} perPage={50} pagination={<Pagination />}>
      <Datagrid rowClick="show" bulkActionButtons={false}>
        <TextField label="amount" source="amount" />
        <ReferenceField
          label="ClaimStatus"
          source="claimstatus.id"
          reference="ClaimStatus"
        >
          <TextField source={CLAIMSTATUS_TITLE_FIELD} />
        </ReferenceField>
        <DateField source="createdAt" label="Created At" />
        <ReferenceField
          label="Customer"
          source="customer.id"
          reference="Customer"
        >
          <TextField source={CUSTOMER_TITLE_FIELD} />
        </ReferenceField>
        <TextField label="date" source="date" />
        <TextField label="description" source="description" />
        <TextField label="ID" source="id" />
        <DateField source="updatedAt" label="Updated At" />{" "}
      </Datagrid>
    </List>
  );
};
