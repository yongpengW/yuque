type PlaceholderPageProps = {
  title: string;
};

export function PlaceholderPage({ title }: PlaceholderPageProps) {
  return (
    <main style={{ padding: 56 }}>
      <h1 style={{ margin: 0, fontWeight: 500 }}>{title}</h1>
    </main>
  );
}
