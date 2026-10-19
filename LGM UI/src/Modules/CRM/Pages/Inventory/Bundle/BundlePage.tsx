import React, { useEffect, useState } from "react";
import { getBundles, deleteBundle, getBundleById } from "./Services/BundleService";
import type { Bundle, BundleListItem } from "./Services/type";
import BundleWizard from "./BundleWizard";
import BundleViewModal from "./BundleViewModal";
import { type TableColumn } from "react-data-table-component";
import { FaEdit, FaTrash, FaEye } from "react-icons/fa";
import ListingLayout from "../../../../../Components/Common/ListingLayout";
import { actionButtonStyles } from "../../../../../Components/Common/buttonStyles";
import { usePermissions } from "../../../../../hooks/usePermissions";

const BundlePage: React.FC = () => {
  const [items, setItems] = useState<BundleListItem[]>([]);
  const [showWizard, setShowWizard] = useState(false);
  const [editData, setEditData] = useState<Bundle | null>(null);
  const [viewData, setViewData] = useState<Bundle | null>(null);
  const [isLoading, setIsLoading] = useState(false);
  const [filterText, setFilterText] = useState("");
  const { hasPermission } = usePermissions();
  const canAdd = hasPermission("Bundle", "CanAdd");
  const canEdit = hasPermission("Bundle", "CanEdit");
  const canDelete = hasPermission("Bundle", "CanDelete");

  const loadBundles = async () => {
    setIsLoading(true);
    try {
      setItems(await getBundles());
    } catch (error) {
      console.error("Failed to load bundles:", error);
    } finally {
      setIsLoading(false);
    }
  };

  useEffect(() => {
    void loadBundles();
    const refresh = () => void loadBundles();
    window.addEventListener("app-refresh", refresh);
    return () => window.removeEventListener("app-refresh", refresh);
  }, []);

  const handleView = async (id: number) => {
    setIsLoading(true);
    try { setViewData(await getBundleById(id)); }
    catch (error) { console.error("Failed to load bundle:", error); window.alert("Could not load bundle details."); }
    finally { setIsLoading(false); }
  };

  const handleEdit = async (id: number) => {
    setIsLoading(true);
    try {
      setEditData(await getBundleById(id));
      setShowWizard(true);
      window.scrollTo({ top: 0, behavior: "smooth" });
    } catch (error) { console.error("Failed to load bundle for editing:", error); window.alert("Could not load bundle for editing."); }
    finally { setIsLoading(false); }
  };

  const handleDelete = async (id: number) => {
    if (!window.confirm("Are you sure you want to delete this bundle?")) return;
    try { await deleteBundle(id); await loadBundles(); }
    catch (error) { console.error("Failed to delete bundle:", error); window.alert("Could not delete bundle."); }
  };

  const columns: TableColumn<BundleListItem>[] = [
    { name: "Sr. No.", cell: (_row, index) => (index ?? 0) + 1, width: "75px" },
    { name: "Bundle ID", selector: (row) => row.bundleId, sortable: true, width: "105px" },
    { name: "Code", selector: (row) => row.code, sortable: true, grow: 1.2 },
    { name: "Name", selector: (row) => row.name, sortable: true, wrap: true, grow: 1.8 },
    { name: "Parent Product ID", selector: (row) => row.parentProductId, sortable: true, width: "155px" },
    { name: "Package Price", cell: (row) => Number(row.packagePrice).toFixed(2), sortable: true, right: true, width: "140px" },
    { name: "Tax Mode", selector: (row) => row.taxMode, sortable: true, width: "130px" },
    { name: "Effective From", selector: (row) => row.effectiveFrom?.slice(0, 10) || "N/A", sortable: true, width: "140px" },
    { name: "Status", cell: (row) => <span className={row.isActive ? "rounded bg-green-100 px-2 py-1 text-green-700" : "rounded bg-gray-100 px-2 py-1 text-gray-600"}>{row.isActive ? "Active" : "Inactive"}</span>, width: "105px" },
    { name: "Actions", cell: (row) => <div className="flex justify-center gap-4">
      <button type="button" title="View" onClick={() => void handleView(row.bundleId)} className={`${actionButtonStyles.base} ${actionButtonStyles.view}`}><FaEye size={16} /></button>
      {canEdit && <button type="button" title="Edit" onClick={() => void handleEdit(row.bundleId)} className={`${actionButtonStyles.base} ${actionButtonStyles.edit}`}><FaEdit size={16} /></button>}
      {canDelete && <button type="button" title="Delete" onClick={() => void handleDelete(row.bundleId)} className={`${actionButtonStyles.base} ${actionButtonStyles.delete}`}><FaTrash size={16} /></button>}
    </div>, width: "140px" },
  ];

  const filteredItems = items.filter((item) => JSON.stringify(item).toLowerCase().includes(filterText.toLowerCase()));

  return <div className="min-h-screen space-y-6 bg-[#f5f6fa] p-4">
    {showWizard && <BundleWizard editData={editData} onSuccess={() => { setShowWizard(false); setEditData(null); void loadBundles(); }} onCancel={() => { setShowWizard(false); setEditData(null); }} />}
    {viewData && <BundleViewModal bundle={viewData} onClose={() => setViewData(null)} />}
    <ListingLayout title="Listing of Bundles" columns={columns} data={filteredItems} isLoading={isLoading} filterText={filterText} onFilterChange={setFilterText} onCreate={canAdd ? () => { setEditData(null); setShowWizard(true); window.scrollTo({ top: 0, behavior: "smooth" }); } : undefined} />
  </div>;
};

export default BundlePage;
