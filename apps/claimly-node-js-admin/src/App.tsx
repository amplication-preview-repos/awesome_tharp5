import React, { useEffect, useState } from "react";
import { Admin, DataProvider, Resource } from "react-admin";
import dataProvider from "./data-provider/graphqlDataProvider";
import { theme } from "./theme/theme";
import Login from "./Login";
import "./App.scss";
import Dashboard from "./pages/Dashboard";
import { UserList } from "./user/UserList";
import { UserCreate } from "./user/UserCreate";
import { UserEdit } from "./user/UserEdit";
import { UserShow } from "./user/UserShow";
import { CustomerList } from "./customer/CustomerList";
import { CustomerCreate } from "./customer/CustomerCreate";
import { CustomerEdit } from "./customer/CustomerEdit";
import { CustomerShow } from "./customer/CustomerShow";
import { ClaimList } from "./claim/ClaimList";
import { ClaimCreate } from "./claim/ClaimCreate";
import { ClaimEdit } from "./claim/ClaimEdit";
import { ClaimShow } from "./claim/ClaimShow";
import { ClaimStatusList } from "./claimStatus/ClaimStatusList";
import { ClaimStatusCreate } from "./claimStatus/ClaimStatusCreate";
import { ClaimStatusEdit } from "./claimStatus/ClaimStatusEdit";
import { ClaimStatusShow } from "./claimStatus/ClaimStatusShow";
import { jwtAuthProvider } from "./auth-provider/ra-auth-jwt";

const App = (): React.ReactElement => {
  return (
    <div className="App">
      <Admin
        title={"Claimly NodeJS"}
        dataProvider={dataProvider}
        authProvider={jwtAuthProvider}
        theme={theme}
        dashboard={Dashboard}
        loginPage={Login}
      >
        <Resource
          name="User"
          list={UserList}
          edit={UserEdit}
          create={UserCreate}
          show={UserShow}
        />
        <Resource
          name="Customer"
          list={CustomerList}
          edit={CustomerEdit}
          create={CustomerCreate}
          show={CustomerShow}
        />
        <Resource
          name="Claim"
          list={ClaimList}
          edit={ClaimEdit}
          create={ClaimCreate}
          show={ClaimShow}
        />
        <Resource
          name="ClaimStatus"
          list={ClaimStatusList}
          edit={ClaimStatusEdit}
          create={ClaimStatusCreate}
          show={ClaimStatusShow}
        />
      </Admin>
    </div>
  );
};

export default App;
