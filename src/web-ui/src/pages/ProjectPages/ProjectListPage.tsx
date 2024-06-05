import {PageLayout} from "../layouts/PageLayout.tsx";
import React from "react";

const _ProjectListPage = () => {
    return (
        <PageLayout>
            <ProductList/>
        </PageLayout>
    )
}
const ProjectListPage = React.memo(_ProjectListPage);
export default ProjectListPage;